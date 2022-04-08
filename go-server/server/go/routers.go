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
	"fmt"
	"net/http"
	"strings"

	"github.com/gorilla/mux"
)

type Route struct {
	Name        string
	Method      string
	Pattern     string
	HandlerFunc http.HandlerFunc
}

type Routes []Route

func NewRouter(svc CustomerService) *mux.Router {
	router := mux.NewRouter().StrictSlash(true)
	api := CustomerServiceApi{service: svc}

	var routes = Routes{
		Route{
			"Index",
			"GET",
			"/",
			Index,
		},

		Route{
			"Get",
			strings.ToUpper("Get"),
			"/CustomerService.svc/GetAllCustomers",
			api.Get,
		},

		Route{
			"Post",
			strings.ToUpper("Post"),
			"/CustomerService.svc/SaveCustomer",
			api.Post,
		},
	}

	for _, route := range routes {
		var handler http.Handler
		handler = route.HandlerFunc
		handler = Logger(handler, route.Name)

		router.
			Methods(route.Method).
			Path(route.Pattern).
			Name(route.Name).
			Handler(handler)
	}

	return router
}

func Index(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Hello World!")
}
