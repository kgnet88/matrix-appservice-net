# Roadmap

## POC

### matrix-appservice-net POC v0.1 (done)

- [x] partial port of matrix-appservice-node to C#: 
    - no registration generation
    - no logging
    - no health endpoint
    - no ephemeral events
    - no server tls certificate
    - no event loop
- [x] .net 7
- [x] use of FastEndpoints
- [x] 100% integration tests for endpoints 
- [x] swagger documentation
- [x] example service
- [x] complete c# documentation for docfx

#### notes:

- registration validation is incomplete
- no mediator, no request validation
- handling of `txnId` must be improved
- error codes should be service specific
- no support for event handling
- handling of query parameter `access_token` not optimal, real auth needed
- saving and loading of the yaml registration needs to be refactored