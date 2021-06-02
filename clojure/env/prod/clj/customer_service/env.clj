(ns customer-service.env
  (:require [clojure.tools.logging :as log]))

(def defaults
  {:init
   (fn []
     (log/info "\n-=[customer-service started successfully]=-"))
   :stop
   (fn []
     (log/info "\n-=[customer-service has shut down successfully]=-"))
   :middleware identity})
