# Email Verification API 📧

[English](README.md) | [Türkçe](README_TR.md)

This API is a simple and effective .NET Core Web API project used for verifying user email addresses. This template is open for development and can be integrated into your own projects.

> 🔍 **Note**: This API serves as a template and is open for further development. You can freely integrate and customize it for your own projects.

## Features ✨

- Send 6-digit verification code to email address
- Verify the validation code
- 5-minute code validity period
- Gmail SMTP integration
- Swagger UI support

## Technologies 🛠

- .NET 8.0
- MailKit (for email sending)
- Swagger/OpenAPI
- Memory Cache

## Installation 🚀

1. Clone the repository:
   ```bash
   git clone https://github.com/kullaniciadi/EmailVerificationAPI.git
   ```

2. Go to project directory:
   ```bash
   cd EmailVerificationAPI
   ```

3. Update your Gmail credentials in `VerificationController.cs`:
   ```csharp
   emailMessage.From.Add(new MailboxAddress("Support", "your-email@gmail.com"));
   await smtp.AuthenticateAsync("your-email@gmail.com", "your-app-password");
   ```

4. Run the project:
   ```bash
   dotnet run
   ```

## API Endpoints 📝

### 1. Send Verification Code
http
POST /api/Verification/send-code
Content-Type: application/json
{
"email": "user@example.com"
}


### 2. Verify Code
http
POST /api/Verification/verify-code
Content-Type: application/json
{
"email": "user@example.com",
"code": "123456"
}


## Security 🔒

- Verification codes automatically expire after 5 minutes
- Codes are removed from the system after successful verification
- Temporary data storage using Memory Cache

## Contributing 🤝

1. Fork this repository
2. Create a new branch (`git checkout -b feature/newFeature`)
3. Commit your changes (`git commit -am 'Added new feature'`)
4. Push to the branch (`git push origin feature/newFeature`)
5. Create a Pull Request

## License 📄

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact 📧

For your questions, you can useyasardgn99@gmail.com
