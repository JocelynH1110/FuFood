// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using JWT.Algorithms;
using JWT.Builder;

var userId = Guid.CreateVersion7();

const string issuer = "FuFood";
const string aud = "FuFood";
var signer = RandomNumberGenerator.GetBytes(32);

var token = JwtBuilder.Create()
    .WithAlgorithm(new HMACSHA256Algorithm())
    .WithSecret(signer)
    .AddClaim("sub", userId.ToString())
    .AddClaim("iss", issuer)
    .AddClaim("aud", aud)
    .Encode();

Console.WriteLine(token);