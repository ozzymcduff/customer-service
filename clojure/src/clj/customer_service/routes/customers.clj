(ns customer-service.routes.customers
    (:require
     [customer-service.customers :as customers]
     [clojure.tools.logging :as log]
     [customer-service.middleware :as middleware]
     [customer-service.schema :as s]
     [ring.util.response]
     [ring.util.http-response :as response]))



(defn get-all [request]
    (response/ok (customers/read-all)
    ))

(defn save [{:keys [body-params] :as request}]
  (let [customer body-params]
    (response/ok (customers/update customer))))

(defn customer-routes []
    [ "/CustomerService.svc" 
        {:middleware [middleware/wrap-formats]}
        ["/GetAllCustomers" {:get get-all}]
        ["/SaveCustomer" {:post save}]
        ])
    