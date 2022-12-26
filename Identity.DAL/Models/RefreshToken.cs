﻿namespace Identity.DAL.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public string UserId { get; set; }

    public string Token { get; set; }
    
    public bool IsUsed { get; set; }
    
    public bool IsRevoked { get; set; }
    
    public DateTime AddedDate { get; set; }
    
    public DateTime ExpiryDate { get; set; }
}