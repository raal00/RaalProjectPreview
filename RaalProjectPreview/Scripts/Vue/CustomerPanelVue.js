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
				items = items.filter(function (row) {
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

var customerPanelVue = new Vue({
	el: '#customerPanelVue',
	data:
	{
		Orders: [],
		GridOrdersCols: ['Id', 'CustomerId', 'OrderDate', 'ShipmentDate', 'OrderNumber', 'Status'],
		Items: [],
		GridItemsCols: ['Id', 'Code', 'Name', 'Price', 'Category'],

		CaseItems: [],
		searchQueryItem: '',
		searchQueryOrder: '',
		OnlyNewOrders: false,
		alertClass: '',
		alertMsg: '',
		showModalAlert: false
	},
	methods:
	{	
		CloseModal: function(){
			var vue = this;
			vue.showModalAlert = false;
		},
		GetItemList: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/GetItemList",
				success: function (response) {
					vue.Items = response.Items;
					if (vue.Items.length == 0) {
						vue.alertMsg = 'No items in shop';
						vue.alertClass = 'text-warning';
						vue.showModalAlert = true;
					}
				}
			});
		},
		AddItemToCase: function (item) {
			var vue = this;
			dataPost = {
				ItemId: item.Id
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Customer/AddItemToCase",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						vue.showModalAlert = true;
					}
					else {
						vue.GetMyCase();
					}
				}
			});
		},
		CreateOrder: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/CreateOrder",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						vue.showModalAlert = true;
					}
					else {
						vue.ShowMyOrders();
						vue.GetMyCase();
					}
				}
			});
		},
		ShowMyOrders: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/ShowMyOrders",
				success: function (response) {
					vue.Orders = response.Orders;
					if (response.Orders == null || response.Orders.length == 0) {
						
						vue.alertMsg = 'No active orders';
						vue.alertClass = 'text-warning';
						vue.showModalAlert = true;
					}
				}
			});
		},
		CloseOrder: function (item) {
			var vue = this;
			dataPost = {
				OrderId: item.Id
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Customer/CloseOrder",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						vue.showModalAlert = true;
					}
					else {
						vue.ShowMyOrders();
						vue.GetMyCase();
					}
				}
			});
		},
		GetMyCase: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/MyCase",
				success: function (response) {
					if (response.CustomerCase.length == 0) {
						vue.alertMsg = 'No items in case';
						vue.alertClass = 'text-warning';
						vue.showModalAlert = true;
					}
					vue.CaseItems = response.CustomerCase;
				}
			});
        }
	},
	computed:
	{
		filteredOrderList() {
			var vue = this;
			return vue.Orders.filter(order => {
				if (order.Status == 'New' || vue.OnlyNewOrders == false) return order;
			})
		}
	},
	mounted() {
		var vue = this;
		vue.GetItemList();
		vue.ShowMyOrders();
	}
});