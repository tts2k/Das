public class Platform
{
    public string distro {get; set;}
    public string arch {get; set;}
    public string kernel {get; set;}

    public Platform(string distro, string arch, string kernel)
    {
        this.distro = distro;
        this.arch = arch;
        this.kernel = kernel;
    }
}
