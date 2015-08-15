﻿namespace Perch
open System
    module Enum=
        let tryParse s :'a option when 'a:enum<'b> =
            match System.Enum.TryParse s with
            | true, v -> Some v
            | _ -> None
        
        let parse v =
            tryParse(v).Value
