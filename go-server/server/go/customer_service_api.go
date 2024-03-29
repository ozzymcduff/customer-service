/*
 * Customer Service
 *
 * Port of the api for demonstrating mvvm found at [galasoft](https://galasoft.ch/labs/Customers/CustomerService.svc).
 *
 * API version: 1.0.0-oas3
 * Generated by: Swagger Codegen (https://github.com/swagger-api/swagger-codegen.git)
 */
package customerservice

import (
	"encoding/json"
	"net/http"
)

type CustomerServiceApi struct {
	service CustomerService
}

func (a *CustomerServiceApi) Get(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	w.WriteHeader(http.StatusOK)
	enc := json.NewEncoder(w)
	cs := ArrayOfCustomer{Customer: a.service.GetAll()}
	enc.Encode(cs)
}

func (a *CustomerServiceApi) Post(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	decoder := json.NewDecoder(r.Body)
	var input Customer
	enc := json.NewEncoder(w)
	err := decoder.Decode(&input)
	if err != nil {
		w.WriteHeader(http.StatusBadRequest)
		enc.Encode(false)
		return
	}
	err = a.service.Add(input)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		enc.Encode(false)
		return
	}
	enc.Encode(true)
}
