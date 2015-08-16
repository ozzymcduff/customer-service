﻿namespace Perch

open System.Xml
open System.Xml.Linq

//open System.Text

(*type A = {
    Name : string
    Namespace : string
}*)
module XNamespace=
    let xsi =
        "http://www.w3.org/2001/XMLSchema-instance"

    let xmlns = XName.Get("xmlns")
    /// create namespace xattribute
    let createA name url=
        new XAttribute(XNamespace.Xmlns + name, url)

    let create url=
        XNamespace.Get(url)


    /// create namespace xattribute
    let createADefault url=
        new XAttribute(xmlns, url)

    let isADefault (ns:XAttribute)=
        ns.Name = xmlns

type XName=
    | Simple of string
    | Namespaced of Namespace:XNamespace * Name:string
    //| Other of A
    with
        /// get XName.Namespaced
        static member ns ns' name=
            XName.Namespaced(ns', name)
        /// get System.Xml.Linq.XName from XName
        static member get name=
            match name with
                | Simple n -> XName.Get(n)
                | Namespaced(ns, n) ->XName.Get(localName=n, namespaceName=ns.NamespaceName)
                //| Other({ Name = n }) -> XName.Get(n)
        /// nil attribute name
        static member nil=
               XName.Namespaced (XNamespace.create XNamespace.xsi, "nil")


module XAttr=
    let create name value=
        XAttribute(name= XName.get name, value= value)
    let nil=
        create XName.nil "true"

module XElem=
    let name (node : XElement)=
        node.Name

    let create name content =
        XElement(XName.get name, box content)

    let elements (node : XElement)=
        node.Elements()

    let withName (node : XElement) name=
        node.Element(XName.get name)

    let valueOf (el: XElement)=
        //let el = node.Element(XName.get name)
        let nil = el.Attributes() 
                   |> Seq.tryFind (fun a->a.Name.LocalName = "nil")

        match nil with
            | None -> el.Value
            | Some v -> if v.Value = "true" then null else el.Value

    let value name (value: obj)=
        let content = 
            match value with
                | null -> box XAttr.nil 
                // I want to do this: Option<_>, but it turns into Option<obj> :
                // | :? Option<_> as x -> if x.IsNone then box XAttr.nil else box( x.Value.ToString() )
                | Option.UnionCase <@ None @> _ -> box XAttr.nil
                | Option.UnionCase <@ Some @> [v] -> box( v.ToString() )
                | _ as v -> box( v.ToString() )
        XElement(XName.get name, content)


module XDoc=
    let create content =
        XDocument(content |> Seq.map box)
    let root (node : XDocument)=
        node.Root

module Xml=

    let parse input=
        XDocument.Parse(input)