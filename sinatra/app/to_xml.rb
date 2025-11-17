# frozen_string_literal: true

$properties = 'AccountNumber AddressCity AddressCountry AddressStreet FirstName Gender LastName PictureUri'.split

def uncapitalize(val)
  val[0, 1].downcase + val[1..-1]
end

def to_ruby_case(val)
  val.split(/([A-Z][a-z]*)/).reject(&:empty?).map(&method(:uncapitalize)).join('_')
end

def serialize_property(property, customer)
  val = customer[to_ruby_case(property).to_sym]
  return "  <#{property}>#{val.to_s.encode(xml: :text)}</#{property}>" unless val.nil?

  "  <#{property} xsi:nil=\"true\"/>"
end

def customer_from_hash(customer)
  $properties.map do |prop|
    [to_ruby_case(prop), customer[prop]]
  end.each_with_object({}) do |nxt, memo|
    memo[nxt[0].to_sym] = nxt[1]
  end
end

def to_customers_xml(customers)
  header = <<~XML_HEADER
    <?xml version="1.0" encoding="utf-8"?>
    <ArrayOfCustomer
        xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        xmlns:xsd="http://www.w3.org/2001/XMLSchema"
        xmlns="http://schemas.datacontract.org/2004/07/Customers">
  XML_HEADER
  serialized = customers.map do |customer|
    properties = $properties.map do |prop|
      serialize_property prop, customer
    end.join("\n")
    "<Customer>\n#{properties}\n</Customer>"
  end.join("\n")
  footer = "\n</ArrayOfCustomer>\n"
  [header, serialized, footer].join
end

def to_success_xml(c)
  "<success>#{c}</success>"
end
