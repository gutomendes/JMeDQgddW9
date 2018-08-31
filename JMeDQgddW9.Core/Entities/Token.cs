using System;

namespace JMeDQgddW9.Core.Entities
{
    public class Token
    {
        /// <summary>
        /// TokenIdentifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Token value
        /// </summary>
        public string TokenValue { get; set; }

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
        public virtual User User { get; set; }
    }
}
