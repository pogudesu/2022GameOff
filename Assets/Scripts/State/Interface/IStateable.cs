namespace State.Interface
{
    public interface IStateable
    {
    
        public void Enter(object obj){}
    
        public void Update(object obj){}
    
        public void Exit(object obj){}
        public void ChangeState(IStateable state){}
        
    }
}