namespace NeuroLingo.Utils.JsonHelper;

public sealed record JsonResponse(
    bool IsSuccess,
    object? Errors = null,
    string? Message = null
    );

