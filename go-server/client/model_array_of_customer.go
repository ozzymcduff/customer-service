/*
 * Customer Service
 *
 * Port of the api for demonstrating mvvm found at [galasoft](https://galasoft.ch/labs/Customers/CustomerService.svc).
 *
 * API version: 1.0.0-oas3
 * Generated by: Swagger Codegen (https://github.com/swagger-api/swagger-codegen.git)
 */
package swagger

type ArrayOfCustomer struct {
	Customer []Customer `json:"customer,omitempty"`
}
