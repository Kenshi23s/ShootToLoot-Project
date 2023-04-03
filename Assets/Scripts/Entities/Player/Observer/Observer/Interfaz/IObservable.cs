public interface IObservable
{
    void Subscribe(IObserver elem);
    void Desubscribe(IObserver elem);
}
