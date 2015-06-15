'use strict';
var Customer = require('./Customer');

function CustomerStore() {
  var c = new Customer();
  c.firstName = 'Oskar';
  c.lastName = 'Gewalli';
  c.accountNumber = 0;
  c.gender = 'Male';
  this.getCustomers = function () {

    return Promise.resolve([c]);
  };
  this.saveCustomer = function (customer) {
    console.log(customer);
    return Promise.resolve(false);
  };
}
module.exports = CustomerStore;