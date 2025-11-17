json.extract! customer, :id, :account_number, :address_city, :address_country, :address_street, :first_name, :gender, :last_name, :picture_uri, :created_at, :updated_at
json.url customer_url(customer, format: :json)
