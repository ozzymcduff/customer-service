namespace Customers
open System
open System.IO
open System.Threading.Tasks
    
    module Serializer = 
        val serialize : CustomerOutput -> string
        val deserialize : Stream -> Task<CustomerInput>

    