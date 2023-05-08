using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CreativeUrge.UndoRedo
{
    public class UndoRedoSystem : MonoBehaviour
    {
        [SerializeField] private Button undoButton;
        [SerializeField] private Button redoButton;
    
        private List<IUndoRedoAction> undoStack = new List<IUndoRedoAction>();
        private List<IUndoRedoAction> redoStack = new List<IUndoRedoAction>();

        private Action OnNewActionRecorded { get; set; }
        
        public static UndoRedoSystem Instance { get; private set; }
        
        private const int MAX_STACK_SIZE = 10;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            AddEventListeners();
            SetButtonsInteractivity();
        }

        private void AddEventListeners()
        {
            undoButton.onClick.AddListener(UndoAction);
            redoButton.onClick.AddListener(RedoAction);
            OnNewActionRecorded += SetButtonsInteractivity;
        }
    
        public void RecordAction(IUndoRedoAction action)
        {
            if (undoStack.Count >= MAX_STACK_SIZE)
            {
                var oldAction = undoStack[0];
                undoStack.RemoveAt(0);
                oldAction.Dispose();
            }

            undoStack.Add(action);
            foreach (var redoAction in redoStack)
            {
                redoAction.Dispose();
            }

            redoStack.Clear();
        
            OnNewActionRecorded?.Invoke();
        }

        private void SetButtonsInteractivity()
        {
            undoButton.interactable = undoStack.Count > 0;
            redoButton.interactable = redoStack.Count > 0;
        }
    
        private void UndoAction()
        {
            if (undoStack.Count > 0)
            {
                IUndoRedoAction action = undoStack[undoStack.Count - 1];
                undoStack.Remove(action);
                redoStack.Add(action);
            
                action.Undo();
            }
        
            SetButtonsInteractivity();
        }

        private void RedoAction()
        {
            if (redoStack.Count > 0)
            {
                IUndoRedoAction action = redoStack[^1]; 
                redoStack.Remove(action);
                undoStack.Add(action);
            
                action.Redo();
            }
        
            SetButtonsInteractivity();
        }

        public void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
            
            SetButtonsInteractivity();
        }
    }
}
