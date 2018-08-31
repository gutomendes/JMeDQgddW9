using System;

namespace JMeDQgddW9.Models
{
    /// <summary>
    /// Token Model
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Token value
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token expiration date 
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Token owner user identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Token owner user
        /// </summary>
        public virtual UserModel User { get; set; }
    }
}
