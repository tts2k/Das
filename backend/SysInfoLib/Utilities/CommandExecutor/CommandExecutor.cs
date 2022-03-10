using System.Diagnostics;

namespace SysInfoLib.Utilities
{
    internal class CommandExecutor 
    {
        ///<summary> Create a proceess and execute the given command </summary>
        ///<param name="command"> Command to execute </param>
        ///<param name="omitStderr"> Determine whether to log error from stderr or not </param>
        ///<returns> Tuple with output fron stdout and stderr, or exeception message process initialization failed </returns>
        public (string output, string error) ExecuteCommand(string command, bool  omitStderr = false) 
        {
            try 
            {
                var proc = new Process();
                proc.StartInfo.FileName = "/bin/sh";
                proc.StartInfo.Arguments = $"-c \"${command.Replace("\"", "\\\"")} \"";
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;

                proc.Start();

                var output = proc.StandardOutput.ReadToEnd();
                var error = proc.StandardError.ReadToEnd();

                return new (output, error);

            }
            catch (Exception ex)
            {
                throw new ProcessInitializationFailed(command, ex.Message);
            }
        }
    }

    ///<summary> Exception to throw when process initialization failed </summary>
    public class ProcessInitializationFailed : Exception
    {
        public ProcessInitializationFailed(string command, string message) : base(createExMesg(command, message)) {}

        private static string createExMesg(string command, string message)
        {
            return $"Execution of {command} failed: {message}";
        }
    }
}
