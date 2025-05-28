# Forward Headers Sample Application

This sample demonstrates how to properly configure and use Forward Headers in an ASP.NET Core Identity Server
application when running behind a reverse proxy.

## Overview

The application shows how to:

- Configure Forward Headers middleware
- Handle X-Forwarded-Proto and X-Forwarded-Host headers
- Set up trusted proxy configurations
- Implement proper security measures

## Configuration

The application uses `ForwardedHeadersOptions` to configure:

- Forwarded headers processing (X-Forwarded-Proto and X-Forwarded-Host)
- Trusted hosts and networks
- Forward limit for proxy hops

## Testing

To test the forward headers functionality, use the included `test.sh` script:

1. First, make the script executable:
   ```bash
   chmod +x test.sh
   ```

2. Run the script:
   ```bash
   ./test.sh
   ```

The script sends a curl request to test the forwarded headers processing. It includes custom headers to simulate a proxy
forwarding scenario.

## Running the Application

1. Start the application:
   ```bash
   dotnet run
   ```

2. Access the application at https://localhost:5001

The `test.sh` command should result in the following output.

```http
HTTP/2 200 
content-type: application/json; charset=UTF-8
date: Wed, 28 May 2025 13:22:35 GMT
server: Kestrel

{"issuer":"http://yourdomain.com","jwks_uri":"http://yourdomain.com/.well-known/openid-configuration/jwks","authorization_endpoint":"http://yourdomain.com/connect/authorize","token_endpoint":"http://yourdomain.com/connect/token","userinfo_endpoint":"http://yourdomain.com/connect/userinfo","end_session_endpoint":"http://yourdomain.com/connect/endsession","check_session_iframe":"http://yourdomain.com/connect/checksession","revocation_endpoint":"http://yourdomain.com/connect/revocation","introspection_endpoint":"http://yourdomain.com/connect/introspect","device_authorization_endpoint":"http://yourdomain.com/connect/deviceauthorization","backchannel_authentication_endpoint":"http://yourdomain.com/connect/ciba","pushed_authorization_request_endpoint":"http://yourdomain.com/connect/par","require_pushed_authorization_requests":false,"frontchannel_logout_supported":true,"frontchannel_logout_session_supported":true,"backchannel_logout_supported":true,"backchannel_logout_session_supported":true,"scopes_supported":["openid","profile","scope1","scope2","offline_access"],"claims_supported":["sub","name","family_name","given_name","middle_name","nickname","preferred_username","profile","picture","website","gender","birthdate","zoneinfo","locale","updated_at"],"grant_types_supported":["authorization_code","client_credentials","refresh_token","implicit","password","urn:ietf:params:oauth:grant-type:device_code","urn:openid:params:grant-type:ciba"],"response_types_supported":["code","token","id_token","id_token token","code id_token","code token","code id_token token"],"response_modes_supported":["form_post","query","fragment"],"token_endpoint_auth_methods_supported":["client_secret_basic","client_secret_post"],"id_token_signing_alg_values_supported":["RS256"],"subject_types_supported":["public"],"code_challenge_methods_supported":["plain","S256"],"request_parameter_supported":true,"request_object_signing_alg_values_supported":["RS256","RS384","RS512","PS256","PS384","PS512","ES256","ES384","ES512","HS256","HS384","HS512"],"prompt_values_supported":["none","login","consent","select_account"],"authorization_response_iss_parameter_supported":true,"backchannel_token_delivery_modes_supported":["poll"],"backchannel_user_code_parameter_supported":true,"dpop_signing_alg_values_supported":["RS256","RS384","RS512","PS256","PS384","PS512","ES256","ES384","ES512"]}%      
```

## Security Considerations

- Always specify trusted proxy IP addresses in production
- Configure appropriate forward limit based on your infrastructure
- Use allowedHosts to restrict incoming host headers
- Implement proper network security measures

## License

This sample is licensed under the MIT license.
