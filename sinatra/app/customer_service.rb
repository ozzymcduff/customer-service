# frozen_string_literal: true

class CustomerService
  def initialize
    @customers = [
      { account_number: 1, first_name: 'Oskar', last_name: 'Gewalli', gender: 'Male' }
      # { account_number: 2,first_name: "Greta", last_name: "Skogsberg" },
      # { account_number: 3,first_name: "Matthias", last_name: "Wallisson" },
      # { account_number: 4,first_name: "Ada", last_name: "Lundborg" },
      # { account_number: 5,first_name: "Daniel", last_name: "Örnvik" },
      # { account_number: 6,first_name: "Johan", last_name: "Irisson" },
      # { account_number: 7,first_name: "Edda", last_name: "Tyvinge" }
    ]
  end

  def get_all_customers
    @customers
  end

  def save_customer(customer)
    account_number = customer[:account_number].to_i
    customer_to_update = @customers.find do |c|
      c[:account_number] == account_number
    end

    if customer_to_update
      customer_to_update.merge!(customer)
    else
      @customers << customer
    end
  end
end
