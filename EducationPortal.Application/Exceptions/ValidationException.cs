namespace EducationPortal.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public List<string> Errors { get; set; } = [];

    public ValidationException(List<string> errors)
    {
        foreach (var error in errors)
        {
            Errors.Add(error);
        }
    }

    public void Add(string error)
    {
        Errors.Add(error);
    }
}