namespace Tests
open System
open Customers
open System.Text
open FluentAssertions
open FluentAssertions.Xml
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open System.Xml.Linq
open System.Net.Http
open System.IO
open Xunit

    type ``ContractTests``() =
        let customer = {Customer.Empty with AccountNumber = 1; FirstName = "Oskar"; LastName = "Gewalli" }
        let ns =  "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://schemas.datacontract.org/2004/07/Customers\""
        let serializedCustomers = String.Format( @"<ArrayOfCustomer {0}>
  <Customer>
    <AccountNumber>1</AccountNumber>
    <AddressCity xsi:nil=""true"" />
    <AddressCountry xsi:nil=""true"" />
    <AddressStreet xsi:nil=""true"" />
    <FirstName>Oskar</FirstName>
    <Gender>Male</Gender>
    <LastName>Gewalli</LastName>
    <PictureUri xsi:nil=""true"" />
  </Customer>
</ArrayOfCustomer>", ns)

        let serialized customer =
            let buffer = Serializer.serialize( CustomerOutput.Single(customer) )
            let s = new MemoryStream()
            let w = new StreamWriter(s)
            w.Write(buffer)
            w.Flush()
            s.Seek(0L, SeekOrigin.Begin) |> ignore
            s

        let should_be_xml_equivalent expected actual =
            ignore(XDocument.Parse(actual).Should()
                    .BeEquivalentTo(XDocument.Parse(expected), "xml equivalent"))

        let context c=
            let svc = CustomerServiceFake(c)
            let builder = WebHostBuilder()
                            .Configure(fun app->HttpAdapter.configuration svc app)
            new TestServer(builder)
 
        let GETXml (server: TestServer) (url:string) = task {
            let! q = server.CreateClient().GetAsync(url) in return! q.Content.ReadAsStringAsync() }
        let POSTXml (server: TestServer) (url:string) c = task {
            let! c = server.CreateClient().PostAsync(url, c) in return! c.Content.ReadAsStringAsync() }



        [<Fact>] member test.
         ``get all customers`` () = task {
            let! result = GETXml (context([|customer|])) "/CustomerService.svc/GetAllCustomers"
            should_be_xml_equivalent serializedCustomers result }
                    
        [<Fact>] member test.
         ``save customer`` () = task {
            use s = new StreamContent(serialized customer)
            let! result = POSTXml (context([|customer|])) "/CustomerService.svc/SaveCustomer" s 
            should_be_xml_equivalent @"<boolean>true</boolean>" result }
