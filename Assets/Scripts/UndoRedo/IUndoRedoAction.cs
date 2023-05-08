namespace CreativeUrge.UndoRedo
{
    public interface IUndoRedoAction
    {
        void Undo();
        void Redo();
        void Dispose();
    }
}