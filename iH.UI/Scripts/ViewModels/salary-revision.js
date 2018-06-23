var rm = null;

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
    var self = this;

    self.definitionId = ko.observable(definitionId);
    self.amount = ko.observable(amount);
    self.selectedDef = ko.observable();

    ko.utils.arrayForEach(rm.DefinitionList(), function (definition) {
        if (definition.definitionId() == definitionId) {
            self.selectedDef = ko.observable(definition);
        }
    });
}


var revisionModel = function () {
    var self = this;
    rm = this;
    self.revisionId = 0;
    self.employeeId = 0;

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
        self.employeeId = employeeId;
        
        $.getJSON("/api/Employee/Detail", { employeeId: employeeId }, function (employee) {
            self.EmployeeName(employee.EmployeeName);
            self.Designation(employee.Designation.DesignationName);
            self.Department(employee.Department.DepartmentName);
            self.Location(employee.Location.LocationName);
        });
        
        $.getJSON("/api/Revision/List", { id: employeeId }, function (revList) {
            self.RevisionList.removeAll();

            $.each(revList, function (index, value) {
                self.RevisionList.push(new SalaryRevision(value.RevisionId, value.RevisedOn, value.RevisedEmployee.EmployeeName));
            });
        });
    }

    self.addRevision = function () {
        self.RevisionDetails.removeAll();
        $("#revisionModal").modal();
    }

    self.editRevision = function (data) {
        self.RevisionDetails.removeAll();
        self.revisionId = data.revisionId();

        $.getJSON("/api/Revision/Details", { id: data.revisionId() }, function (revList) {
            $.each(revList, function (index, value) {
                self.RevisionDetails.push(new RevisionDetail(value.DefinitionId, value.Amount));
            });
        });

        $("#revisionModal").modal();
    }

    self.saveRevision = function () {
        var revisionDetails = [];
        ko.utils.arrayForEach(self.RevisionDetails(), function (revision) {
            console.log(revision.selectedDef());

            revisionDetails.push(
                {
                    RevisionId: self.revisionId,
                    DefinitionId: revision.selectedDef().definitionId(),
                    Amount: revision.amount()
                })
        });

        $.ajax({
            dateType: "json",
            contentType: "application/json",
            method: "POST",
            url: "/api/Revision/Save",
            data: JSON.stringify(revisionDetails),
            success: function (result) {
                $('#revisionModal').modal('hide');
            },
            error: function (jqXHR, exception) {
                console.log(exception);
            }
        })
    }

    self.removeDefinition = function () {
        self.RevisionDetails.remove(this);
    }

    self.addDefinition = function () {
        self.RevisionDetails.push(new RevisionDetail(0, 0));
    }
}