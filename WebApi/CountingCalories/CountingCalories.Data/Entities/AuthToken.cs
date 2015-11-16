using System;

namespace CountingCalories.Data.Entities
{
  public class AuthToken
  {
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public ApiUser ApiUser { get; set; }
  }
}
