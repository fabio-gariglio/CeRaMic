

window.onload = function () {

	var enums = {

		Parse: function(enumType, value) {

			for (var item in enumType) {
				if (enumType[item] == value) {
					return item;
				}
			}

			return null;

		},

		RelationType: {
			None: 0,
			Ownership: 1,
			Partnership: 2
		}

	};

	var consts = {

		NullId: "00000000-0000-0000-0000-000000000000"

	};

	window.Enums = enums;
	window.Consts = consts;

};

