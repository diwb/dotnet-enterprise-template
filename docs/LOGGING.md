# Logging

The API uses Serilog for structured logging and ASP.NET Core request logging.

## Correlation IDs

`CorrelationIdMiddleware` reads or creates an `X-Correlation-Id` value and adds it to the response. The same value is pushed into the Serilog log context.

## Request Logging

`UseSerilogRequestLogging` records request method, path, status code and elapsed time.

## Sensitive Data

Never log:

- Passwords
- Access tokens
- Refresh tokens
- Authorization headers
- Payment details
- Personal document numbers

## Production Recommendations

- Send logs to a centralized platform.
- Keep structured properties stable.
- Add alerting for repeated 5xx responses and unhealthy checks.
- Sample noisy successful requests when traffic grows.
