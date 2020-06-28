# Orchard Distributed Events



## About

Orchard module which broadcasts events in a multi-server environment.


## Features

- Generic services for event propagation in a server farm
- Shell change propagation (e.g. shell settings, state of enabled features/modules)
- Signal propagation (i.e. CacheManager changes are replicated)
- Polling implementation for generic, reliable event propagation (that, however, can be swapped out with an implementation optimal for a specific environment)

Distributed Events is part of the [Lombiq Hosting Suite](http://dotnest.com/knowledge-base/topics/lombiq-hosting-suite), a suite of modules making Orchard scale better, be more robust, and have improved maintainability.


## Documentation

**The module depends on [Helpful Libraries](https://github.com/Lombiq/Helpful-Libraries)** (the latest version, from the Orchard 1.8 branch), **so install that first!**

This module adds services to make Orchard better compatible with a multi-server setup: in the core it's a mechanism to fire events that will be also raised on other server nodes. Distributed Events is part of the [Lombiq Hosting Suite](http://dotnest.com/knowledge-base/topics/lombiq-hosting-suite) that makes Orchard fully compatible with multi-node setups and adds many other features to make Orchard better scalable and maintainable. Distributed Events also runs under [DotNest](http://dotnest.com/), the Orchard SaaS.

**Warning!** The module is only compatible with Orchard 1.8!


## Contributing and support

Bug reports, feature requests, comments, questions, code contributions, and love letters are warmly welcome, please do so via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines) while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through Lombiq.
