SectionTitle("About Samantah Jones");
string name = "Samantah Jones";

int lengthOfFirst = name.IndexOf(' ');
int lengthOfLast = name.Length - lengthOfFirst - 1;

string firstName = name.Substring(
    startIndex: 0,
    length: lengthOfFirst);

string lastName = name.Substring(
    startIndex: name.Length - lengthOfLast,
    length: lengthOfLast);

Console.WriteLine($"First: {firstName}, Last: {lastName}");

ReadOnlySpan<char> nameAsSpan = name.AsSpan();
ReadOnlySpan<char> firstNameSpan = nameAsSpan[0..lengthOfFirst];
ReadOnlySpan<char> lastNameSpan = nameAsSpan[^lengthOfLast..];

Console.WriteLine($"First: {firstNameSpan} Last: {lastNameSpan}");

SectionTitle("Compressing streams");
Compress(algorithm: "gzip");
Compress(algorithm: "brotli");