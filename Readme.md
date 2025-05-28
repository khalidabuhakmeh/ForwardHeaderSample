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

## Security Considerations

- Always specify trusted proxy IP addresses in production
- Configure appropriate forward limit based on your infrastructure
- Use allowedHosts to restrict incoming host headers
- Implement proper network security measures

## License

This sample is licensed under the MIT license.
