﻿using System.Collections.Generic;

namespace Nop.Services.Customers 
{
    /// <summary>
    /// Customerregistration result
    /// </summary>
    public class CustomerRegistrationResult 
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CustomerRegistrationResult() 
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets a value indicating whether request has been completed successfully
        /// </summary>
        public bool Success 
        {
            get { return Errors.Count == 0; }
        }

        /// <summary>
        /// Add error
        /// </summary>
        /// <param name="error">Error</param>
        public void AddError(string error) 
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Errors
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}
