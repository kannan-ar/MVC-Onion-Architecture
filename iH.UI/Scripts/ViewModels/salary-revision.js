
var SalaryRevisionDefinition = function (defId, amount) {
    var self = this;

    self.definitionId = ko.observable(defId);
    self.amount = ko.observable(amount);
}

var SalaryRevision = function (revisionId, revisedOn, revisedBy) {
    var self = this;

    self.revisionId = ko.observable(revisionId);
    self.revisedOn = ko.observable(revisedOn);
    self.revisedBy = ko.observable(revisedBy);
}

var RevisionDetail = function (definitionId, amount) {
    self.definitionId = ko.observable(definitionId);
    self.amount = ko.observable(amount);
    self.selectedDef = ko.observable(new SalaryDefinition(definitionId, 0, "HRA1"));
}

var revisionModel = function () {
    var self = this;

    self.EmployeeName = ko.observable(null);
    self.Designation = ko.observable(null);
    self.Department = ko.observable(null);
    self.Location = ko.observable(null);

    self.selectedDefinition = ko.observable();
    self.DefinitionList = ko.observableArray();
    self.RevisionList = ko.observableArray();
    self.RevisionDetails = ko.observableArray();

    self.init = function () {
        $.getJSON("/api/Definition/List", function (defList) {
            $.each(defList, function (index, value) {
                self.DefinitionList.push(
                    new SalaryDefinition(value.DefinitionId, value.RowId, value.DefinitionName, value.Formula, value.FormulaPercentage, value.Minimum,
                    value.Maximum, value.IsDeduction, value.IsVariable, value.IsIncentive, value.IsLOP));
            });

        });
    }

    self.load = function (employeeId) {
        $.getJSON("/api/Revision/List", { id: employeeId }, function (revList) {

            $.each(revList, function (index, value) {
                if (index == 0) {
                    self.EmployeeName(value.Employee.EmployeeName);
                    self.Designation(value.Employee.Designation.DesignationName);
                    self.Department(value.Employee.Department.DepartmentName);
                    self.Location(value.Employee.Location.LocationName);
                }
                self.RevisionList.push(new SalaryRevision(value.RevisionId, value.RevisedOn, value.RevisedEmployee.EmployeeName));
            });
        });
    }

    self.addRevision = function (data) {
        $.getJSON("/api/Revision/Details", { id: data.revisionId() }, function (revList) {
            $.each(revList, function (index, value) {
                self.RevisionDetails.push(new RevisionDetail(value.DefinitionId, value.Amount));
            });
        });

        $("#revisionModal").modal();
    }

    self.removeDefinition = function () {

    }
}