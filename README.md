# About
This library provides a low-level implementation of the HTTP protocol.

This library is intended for users with advanced knowledge of HTTP as well as connection-oriented protocols in general.

# Background and Goals

HTTP is typically spoken over TCP, but it is fundamentally transport agnostic. Most (if not all) HTTP client libraries, including the .NET framework's `System.Net.WebRequest` API, are hard-coded to use TCP/IP sockets.

The key goal for this project is to allow any `System.IO.Stream` to be used as the transport channel. This opens up tons of new possibilities. For example: HTTP over Bluetooth.

# Specifications
Code is based on the latest RFCs regarding version HTTP/1.1.  

* RFC 7230: [Message Syntax and Routing](http://tools.ietf.org/html/rfc7230)
* RFC 7231: [Semantics and Content](http://tools.ietf.org/html/rfc7231)
* RFC 7232: [Conditional Requests](http://tools.ietf.org/html/rfc7232)
* RFC 7233: [Range Request](http://tools.ietf.org/html/rfc7233)
* RFC 7234: [Caching](http://tools.ietf.org/html/rfc7234)
* RFC 7235: [Authentication](http://tools.ietf.org/html/rfc7235)
* RFC 7236: [Authentication Scheme Registrations](http://tools.ietf.org/html/rfc7236)
* RFC 7237: [Method Registrations](http://tools.ietf.org/html/rfc7237)
* RFC 7238: [the 308 status code](http://tools.ietf.org/html/rfc7238)
* RFC 7239: [Forwarded HTTP extension](http://tools.ietf.org/html/rfc7239)