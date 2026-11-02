var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// POST /api/docs: takes code, returns documentation.
// For now it returns fake data so we can build the frontend against it.
app.MapPost("/api/docs", (DocRequest request) =>
{
    // Reject empty input before doing any work
    if (string.IsNullOrWhiteSpace(request.Code))
        return Results.BadRequest(new { error = "Code is required." });

    var fake = new DocResponse(
        Summary: "Fake summary. The LLM will write this later.",
        Functions:
        [
            new FunctionDoc("Add", ["int a", "int b"], "int", "Add(2, 3) // returns 5")
        ]);

    return Results.Ok(fake);
});

app.Run();

// The shape of what comes in and what goes out
record DocRequest(string Code, string Language);
record DocResponse(string Summary, List<FunctionDoc> Functions);
record FunctionDoc(string Name, List<string> Parameters, string Returns, string Example);