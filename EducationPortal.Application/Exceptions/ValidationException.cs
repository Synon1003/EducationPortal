namespace EducationPortal.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public List<string> Errors { get; set; } = [];

    public ValidationException(List<string> errors)
    {
        Errors.AddRange(errors);
    }

    public void Add(string error)
    {
        Errors.Add(error);
    }
}