using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingUI
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SelectCommand { get; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SelectCommand = new DelegateCommand(OnSelectCommand);
        }

        public event EventHandler HideRequest;
        private void RaiseHideRequest()
        {
            HideRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ShowRequest;
        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }

        private void OnSelectCommand()
        {
            RaiseHideRequest();

            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var selectedObject = uidoc.Selection.PickObject(ObjectType.Element, "Выберите элемент");
            var oElement = doc.GetElement(selectedObject);

            TaskDialog.Show("Сообщение", $"ID: {oElement.Id}");

            RaiseShowRequest();
        }
    }
}
