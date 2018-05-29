var SalaryDefinition = function (defId, rowId, defName, formula, formulaPercentage, minimum, maximum, isDeduction, isVariable, isIncentive, isLOP) {
    var self = this;

    self.definitionId = ko.observable(defId);
    self.rowId = ko.observable(rowId);
    self.definitionName = ko.observable(defName);
    self.formula = ko.observable(formula);
    self.formulaPercentage = ko.observable(formulaPercentage);
    self.minimum = ko.observable(minimum);
    self.maximum = ko.observable(maximum);
    self.isDeduction = ko.observable(isDeduction);
    self.isVariable = ko.observable(isVariable);
    self.isIncentive = ko.observable(isIncentive);
    self.isLOP = ko.observable(isLOP);
}

var viewModel = function () {
    var self = this;

    self.Definition = new SalaryDefinition();
    self.DefinitionList = ko.observableArray();

    self.load = function () {
        $.getJSON("/api/Definition/List", function (data) {
            $.each(data, function (index, value) {
                self.DefinitionList.push(
                    new SalaryDefinition(value.DefinitionId, value.RowId, value.DefinitionName, value.Formula, value.FormulaPercentage, value.Minimum,
                    value.Maximum, value.IsDeduction, value.IsVariable, value.IsIncentive, value.IsLOP));
            });
        });
    }

    self.add = function () {

        self.Definition.definitionId(null);
        self.Definition.rowId(null);
        self.Definition.definitionName("");
        self.Definition.formula("");
        self.Definition.formulaPercentage("");
        self.Definition.minimum("");
        self.Definition.maximum("");
        self.Definition.isDeduction(false);
        self.Definition.isVariable(false);
        self.Definition.isIncentive(false);
        self.Definition.isLOP(false);

        $("#definitionModal").modal();
    }

    self.edit = function (item) {
        self.Definition.rowId(item.rowId());
        self.Definition.definitionId(item.definitionId());
        self.Definition.definitionName(item.definitionName());
        self.Definition.formula(item.formula());
        self.Definition.formulaPercentage(item.formulaPercentage());
        self.Definition.minimum(item.minimum());
        self.Definition.maximum(item.maximum());
        self.Definition.isDeduction(item.isDeduction());
        self.Definition.isVariable(item.isVariable());
        self.Definition.isIncentive(item.isIncentive());
        self.Definition.isLOP(item.isLOP());

        $("#definitionModal").modal();
    }

    self.save = function () {
        
        $.post("/api/Definition/Save", {
            DefinitionId: self.Definition.definitionId(),
            RowId: self.Definition.rowId(),
            DefinitionName: self.Definition.definitionName(),
            Formula: self.Definition.formula(),
            FormulaPercentage: self.Definition.formulaPercentage(),
            Minimum: self.Definition.minimum(),
            Maximum: self.Definition.maximum(),
            IsDeduction: self.Definition.isDeduction(),
            IsVariable: self.Definition.isVariable(),
            IsIncentive: self.Definition.isIncentive(),
            IsLOP: self.Definition.isLOP()
        }).done(function (data) {
            var found = false;

            ko.utils.arrayForEach(self.DefinitionList(), function (item) {
                if (item.definitionId() === data.DefinitionId) {

                    found = true;

                    item.rowId(data.RowId);
                    item.definitionName(data.DefinitionName);
                    item.formula(data.Formula);
                    item.formulaPercentage(data.FormulaPercentage);
                    item.minimum(data.Minimum);
                    item.maximum(data.Maximum);
                    item.isDeduction(data.IsDeduction);
                    item.isVariable(data.IsVariable);
                    item.isIncentive(data.IsIncentive);
                    item.isLOP(data.IsLOP);
                }
            });

            if (found === false) {
                self.DefinitionList.push(
                   new SalaryDefinition(data.DefinitionId, data.RowId, data.DefinitionName, data.Formula, data.FormulaPercentage,
                    data.Minimum, data.Maximum, data.IsDeduction, data.IsVariable, data.IsIncentive, data.IsLOP));
            }

            $("#definitionModal").modal('hide');
        })
    }
}