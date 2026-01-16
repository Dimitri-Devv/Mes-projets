namespace Breeder;

public interface InconnuProvider<out T>
{
    T GetInconnu();
}