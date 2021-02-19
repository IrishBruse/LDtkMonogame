// Choose example
#define GameExample
//#define ApiExample

namespace Examples
{
    class Program
    {
        static void Main()
        {
#if GameExample
            new LDtkExample().Run();
#endif

#if ApiExample
            new ApiExample().Run();
#endif
        }
    }
}