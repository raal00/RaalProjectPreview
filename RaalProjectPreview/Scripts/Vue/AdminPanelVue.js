Vue.component("order-grid", {
	template: "#orders-template",
	props: {
		orders: [],
		columns: [],
		filterKey: ''
	},
	data: function () {
		var sortOrders = {};
		this.columns.forEach(function (key) {
			sortOrders[key] = 1;
		});
		console.log(sortOrders);
		return {
			sortKey: '',
			sortOrders: sortOrders
		};
	},

	computed: {
		filteredOrders: function () {
			var sortKey = this.sortKey;
			var filterKey = this.filterKey && this.filterKey.toLowerCase();
			var order = this.sortOrders[sortKey] || 1;
			var orders = this.orders;
			if (filterKey) {
				orders = orders.filter(function (row) {
					return Object.keys(row).some(function (key) {
						return (
							String(row[key])
								.toLowerCase()
								.indexOf(filterKey) > -1
						);
					});
				});
			}
			if (sortKey) {
				orders = orders.slice().sort(function (a, b) {
					a = a[sortKey];
					b = b[sortKey];
					return (a === b ? 0 : a > b ? 1 : -1) * order;
				});
			}
			return orders;
		}
	},
	filters: {
		capitalize: function (str) {
			return str.charAt(0).toUpperCase() + str.slice(1);
		}
	},
	methods: {
		sortBy: function (key) {
			this.sortKey = key;
			this.sortOrders[key] = this.sortOrders[key] * -1;
		}
	}
});
Vue.component("item-grid", {
	template: "#items-template",
	props: {
		items: [],
		columns: [],
		filterKey: ''
	},
	data: function () {
		var sortOrders = {};
		this.columns.forEach(function (key) {
			sortOrders[key] = 1;
		});
		console.log(sortOrders);
		return {
			sortKey: '',
			sortOrders: sortOrders
		};
	},

	computed: {
		filteredItems: function () {
			var sortKey = this.sortKey;
			var filterKey = this.filterKey && this.filterKey.toLowerCase();
			var order = this.sortOrders[sortKey] || 1;
			var items = this.items;
			if (filterKey) {
				items = users.filter(function (row) {
					return Object.keys(row).some(function (key) {
						return (
							String(row[key])
								.toLowerCase()
								.indexOf(filterKey) > -1
						);
					});
				});
			}
			if (sortKey) {
				items = items.slice().sort(function (a, b) {
					a = a[sortKey];
					b = b[sortKey];
					return (a === b ? 0 : a > b ? 1 : -1) * order;
				});
			}
			return items;
		}
	},
	filters: {
		capitalize: function (str) {
			return str.charAt(0).toUpperCase() + str.slice(1);
		}
	},
	methods: {
		sortBy: function (key) {
			this.sortKey = key;
			this.sortOrders[key] = this.sortOrders[key] * -1;
		}
	}
});
Vue.component("user-grid", {
	template: "#users-template",
	props: {
		users: [],
		columns: [],
		filterKey: ''
	},
	data: function () {
		var sortOrders = {};
		this.columns.forEach(function (key) {
			sortOrders[key] = 1;
		});
		console.log(sortOrders);
		return {
			sortKey: '',
			sortOrders: sortOrders
		};
	},
	
	computed: {
		filteredUsers: function () {
			var sortKey = this.sortKey;
			var filterKey = this.filterKey && this.filterKey.toLowerCase();
			var order = this.sortOrders[sortKey] || 1;
			var users = this.users;
			if (filterKey) {
				users = users.filter(function (row) {
					return Object.keys(row).some(function (key) {
						return (
							String(row[key])
								.toLowerCase()
								.indexOf(filterKey) > -1
						);
					});
				});
			}
			if (sortKey) {
				users = users.slice().sort(function (a, b) {
					a = a[sortKey];
					b = b[sortKey];
					return (a === b ? 0 : a > b ? 1 : -1) * order;
				});
			}
			return users;
		}
	},
	filters: {
		capitalize: function (str) {
			return str.charAt(0).toUpperCase() + str.slice(1);
		}
	},
	methods: {
		sortBy: function (key) {
			this.sortKey = key;
			this.sortOrders[key] = this.sortOrders[key] * -1;
		}
	}
});

var adminPanelVue = new Vue({
	el: '#adminPanelVue',
	data:
	{
		Users: [],
		GridUsersCols: ['CustomerId', 'Name', 'Code', 'Address', 'Discount', 'Login', 'PasswordHash', 'ClientRole'],
		Items: [],
		GridItemsCols: ['Id', 'Code', 'Name', 'Price', 'Category'],
		Orders: [],
		GridOrdersCols: ['Id', 'CustomerId', 'OrderDate', 'ShipmentDate', 'OrderNumber', 'Status'],

		searchQueryUser: '',
		searchQueryItem: '',
		searchQueryOrder: '',
		
		NewItem: {
			Id: -1,
			Code: 'XX-XXXX-YYXX',
			Name: '',
			Price: 0,
			Category: ''
		},
		NewUser: {
			CustomerId: -1,
			Name: '',
			Code: 'XXXX-YYYY',
			Address: '',
			Discount: 0.0,
			Login: '',
			PasswordHash: '',
			ClientRole: 0
		},
		editMod: Boolean,
		alertClass: '',
		alertMsg: '',
		showModalUser: false,
		showModalItem: false,
		showModalAlert: false
	},
	methods:
	{
		CloseModal: function(){
			var vue = this;
			vue.showModalUser = false;
			vue.showModalItem = false;
			vue.showModalAlert = false;
		},
		ShowAllUsers: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Admin/ShowUsers",
				success: function (response) {
					vue.Users = response.UserList;
					if (vue.Users.length == 0) {
						vue.alertMsg = 'No users';
						vue.alertClass = 'text-warning';
						vue.showModalAlert = true;
					}
				}
			});
        },
		ShowAllOrders: function() {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Admin/ShowOrders",
				success: function (response) {
					vue.Orders = response.Orders;
					if (vue.Orders.length == 0) {
						vue.alertMsg = 'No orders';
						vue.alertClass = 'text-warning';
						vue.showModalAlert = true;
					}
				}
			});
		},
		ShowAllItems: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Admin/ShowItems",
				success: function (response) {
					vue.Items = response.Items;
					if (vue.Items.length == 0) {
						vue.alertMsg = 'No items';
						vue.alertClass = 'text-warning';
						vue.showModalAlert = true;
					}
				}
			});
		},

		StartAddNewUser: function () {
			var vue = this;
			vue.editMod = false;
			vue.NewUser.Name = '';
			vue.NewUser.Code = 'XXXX-YYYY';
			vue.NewUser.Address = '';
			vue.NewUser.Discount = 0;
			vue.NewUser.Login = '';
			vue.NewUser.PasswordHash = '';
			vue.NewUser.ClientRole = 0;
			vue.showModalUser = true;
		},
		StartEditUser: function (user) {
			var vue = this;
			vue.editMod = true;
			vue.NewUser.CustomerId = user.CustomerId;
			vue.NewUser.Name = user.Name;
			vue.NewUser.Code = user.Code;
			vue.NewUser.Address = user.Address;
			vue.NewUser.Discount = user.Discount;
			vue.NewUser.Login = user.Login;
			vue.NewUser.PasswordHash = user.PasswordHash;
			vue.NewUser.ClientRole = user.ClientRole;
			vue.showModalUser = true;
		},
		

		StartAddNewItem: function () {
			var vue = this;
			vue.editMod = false;
			vue.NewItem.Code = 'XX-XXXX-YYXX';
			vue.NewItem.Name = '';
			vue.NewItem.Price = 0;
			vue.NewItem.Category = '';
			vue.showModalItem = true;
		},
		StartEditItem: function (item) {
			var vue = this;
			vue.editMod = true;
			vue.NewItem.Id = item.Id;
			vue.NewItem.Code = item.Code;
			vue.NewItem.Name = item.Name;
			vue.NewItem.Price = item.Price;
			vue.NewItem.Category = item.Category;
			vue.showModalItem = true;
		},
		
		DeleteUser: function (user) {
			var vue = this;
			dataPost = {
				UserId: user.CustomerId
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Admin/DeleteUser",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						vue.showModalAlert = true;
					}
					else {
						vue.ShowAllUsers();
					}
				}
			});
		},
		DeleteItem: function (item) {
			var vue = this;
			dataPost = {
				ItemId: item.Id
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Admin/DeleteItemFromShop",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						vue.showModalAlert = true;
					}
					else {
						vue.ShowAllItems();
					}
				}
			});
		},

		SetCompletedOrderStatus: function (order) {
			var vue = this;
			dataPost = {
				OrderId: order.Id
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Admin/SetCompletedOrderStatus",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						vue.showModalAlert = true;
					}
					vue.ShowAllOrders();
				}
			});
        },
		SetInProcessingOrderStatus: function (order) {
			var vue = this;
			dataPost = {
				OrderId: order.Id
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Admin/SetInProcessingOrderStatus",
				success: function (response) {
					console.log(response);
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						vue.showModalAlert = true;
					}
					vue.ShowAllOrders();
				}
			});
		}
	},
	mounted() {
		var vue = this;
		vue.ShowAllUsers();
		vue.ShowAllItems();
		vue.ShowAllOrders();
	}
});