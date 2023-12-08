namespace MarvelLearningAPI.Event;

public record CreateUserRequest(string FirstName, string LastName, bool Internal);