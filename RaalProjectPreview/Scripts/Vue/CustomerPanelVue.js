Vue.component("order-grid", {
	template: '<table><thead><tr><th v-for="key in columns"v-on:click="sortBy(key)":class="{ active: sortKey == key }">{{ key | capitalize }}<span class="arrow asc"></span></th><th></th></tr></thead><tbody><tr v-for="entry in filteredOrders"><td v-for="key in columns">{{ entry[key] }}</td><td><button type="button" class="btn btn-default text-primary" v-on:click="customerPanelVue.CloseOrder(entry)">Close</button></td></tr></tbody></table>',
	props: {
		orders: [],
		columns: [],
		filterKey: '',
		by: '1'
	},
	data: function () {
		var sortOrders = {};
		this.columns.forEach(function (key) {
			sortOrders[key] = 1;
		});
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
			var bystr = String(this.by);
			if (filterKey) {
				orders = orders.filter(function (ord) {
					switch (bystr) {
						case 'Id':
							return ord.Id.toLowerCase().indexOf(filterKey) > -1;
						case 'CustomerId':
							return ord.CustomerId.toLowerCase().indexOf(filterKey) > -1;
						case 'OrderDate':
							return ord.OrderDate.toLowerCase().indexOf(filterKey) > -1;
						case 'ShipmentDate':
							return ord.ShipmentDate.toLowerCase().indexOf(filterKey) > -1;
						case 'OrderNumber':
							return ord.OrderNumber.toLowerCase().indexOf(filterKey) > -1;
						case 'Status':
							return ord.Status.toLowerCase().indexOf(filterKey) > -1;
						default:
							return true;
					}
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
	template: '<table><thead><tr><th v-for="key in columns"v-on:click="sortBy(key)":class="{ active: sortKey == key }">{{ key | capitalize }}<span class="arrow asc"></span></th><th></th></tr></thead><tbody><tr v-for="entry in filteredItems"><td v-for="key in columns">{{ entry[key] }}</td><td><button type="button" class="btn btn-default" v-on:click="customerPanelVue.AddItemToCase(entry)">add to case</button></td></tr></tbody></table>',
	props: {
		items: [],
		columns: [],
		filterKey: '',
		by: '1'
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
			var bystr = String(this.by);
			if (filterKey) {
				items = items.filter(function (itm) {
					switch (bystr) {
						case 'Id':
							return itm.Id.toLowerCase().indexOf(filterKey) > -1;
						case 'Code':
							return itm.Code.toLowerCase().indexOf(filterKey) > -1;
						case 'Name':
							return itm.Name.toLowerCase().indexOf(filterKey) > -1;
						case 'Price':
							return itm.Price.toLowerCase().indexOf(filterKey) > -1;
						case 'Category':
							return itm.Category.toLowerCase().indexOf(filterKey) > -1;
						default:
							return true;
					}
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
		showModalAlert: false,
		searchItemBy: '',
		searchOrderBy: '',
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