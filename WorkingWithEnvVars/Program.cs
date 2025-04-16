SectionTitle("Reading all environment variables for process");
IDictionary vars = GetEnvironmentVariables();
DictionaryToTable(vars);

SectionTitle("Reading all environment variables for machine");
IDictionary varsMachine = GetEnvironmentVariables(
EnvironmentVariableTarget.Machine);
DictionaryToTable(varsMachine);
SectionTitle("Reading all environment variables for user");
IDictionary varsUser = GetEnvironmentVariables(
EnvironmentVariableTarget.User);
DictionaryToTable(varsUser);