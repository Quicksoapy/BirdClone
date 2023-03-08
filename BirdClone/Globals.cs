using System.Collections;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace BirdClone;

public class Globals
{
    public static string GetSha512(string input)
    {
        var hash = "";
        var alg = SHA512.Create();
        var result = alg.ComputeHash(Encoding.UTF8.GetBytes(input));
        hash = Encoding.UTF8.GetString(result);
        return hash;
    }
}