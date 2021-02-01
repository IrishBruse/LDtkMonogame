// Choose example
#define GameExample
//#define NugetExample

namespace Examples
{
    class Program
    {
        static void Main()
        {
#if GameExample
            new LDtkExample().Run();
#endif

#if NugetExample
            new NugetExample().Run();
#endif
        }
    }
}