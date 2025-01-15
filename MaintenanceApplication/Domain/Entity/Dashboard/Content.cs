using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class Content
    {
        public Guid Id { get; set; }             
        public string Title { get; set; }          // Title of the content (e.g., Terms of Service, Privacy Policy)
        public string Body { get; set; }           // The body/content (full text of the policy, guidelines, etc.)
        public ContentType ContentType { get; set; }    // Type of content (e.g., Policy, Guidelines, FAQ)
        public bool IsActive { get; set; }         // If the content is active (can be visible to users)
        public DateTime CreatedAt { get; set; }    // When the content was created
        public DateTime? UpdatedAt { get; set; }   // When the content was last updated (nullable)
    }

    #region Enums
    public enum ContentType
    {
        Policy = 1,       // For Privacy Policy, Terms of Service, etc.
        Guidelines = 2,   // For user guidelines, best practices, etc.
        FAQ = 3,          // For frequently asked questions
        Other = 4         // For any other type of content
    }

    #endregion

    public class ContentValidator : AbstractValidator<Content>
    {
        public ContentValidator()
        {
            // Rule for Title: It must not be empty and have a minimum and maximum length.
            RuleFor(content => content.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(5, 100).WithMessage("Title must be between 5 and 100 characters.");

            // Rule for Body: It must not be empty.
            RuleFor(content => content.Body)
                .NotEmpty().WithMessage("Body is required.");

            // Rule for ContentType: It must be a valid enum value.
            RuleFor(content => content.ContentType)
                .IsInEnum().WithMessage("Invalid content type.");

            // Rule for IsActive: No special validation needed as it's a boolean.

            // Rule for CreatedAt: Must not be a future date.
            RuleFor(content => content.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedAt cannot be in the future.");

            // Rule for UpdatedAt: If provided, must not be a future date and should be after CreatedAt.
            RuleFor(content => content.UpdatedAt)
                .Must((content, updatedAt) => updatedAt == null || updatedAt >= content.CreatedAt)
                .When(content => content.UpdatedAt.HasValue)
                .WithMessage("UpdatedAt must be null or after CreatedAt.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("UpdatedAt cannot be in the future.");
        }
    }

}
