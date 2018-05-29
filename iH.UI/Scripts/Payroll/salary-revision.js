$("#search").typeahead({
    source: function (query, process) {
        return $.getJSON("/api/Employee/List", { query: query }, function (data) {
            return process(data);
        });
    },
    displayText: function (item) {
        return item.EmployeeName;
    },
    afterSelect: function (item) {
        history.pushState(null, "", '/Payroll/Salary/Revision/' + item.EmployeeId);
        vm.load(item.EmployeeId);
    }
});

var vm = new revisionModel();
vm.init();
ko.applyBindings(vm);