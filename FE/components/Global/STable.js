import T from "ant-design-vue/es/table/Table";
// import FcEmpty from '../Shared/FcEmpty.vue';

export default {
  props: Object.assign({}, T.prTableops, {
    columns: {
      type: Array,
      default: [],
    },
    dataSource: {
      type: Array,
      default: [],
    },
    onTableChange: {
      type: Function,
    },
    loading: {
      type: Boolean,
      default: false,
    },
    pagination: {
      type: Object,
      default: {},
    },
    rowKey: {
      type: String,
      default: "id",
    },
    pageSizeForClientSide: {
      type: Number,
      default: 10,
    },
    components: {
      type: Object,
    },
    rowSelection: {
      type: Object,
    },
  }),
  methods: {
    // emptyText() {
    //   return <FcEmpty />;
    // },
    itemRender(current, type, originalElement) {
      if (type === "prev") {
        return <a class="fc-pagination-btn">Previous</a>;
      } else if (type === "next") {
        return <a class="fc-pagination-btn">Next</a>;
      }
      return originalElement;
    },
    handleTableChange(pagination, filters, sorter) {
      this.$emit("onTableChange", {
        pagination,
        filters,
        sorter,
      });
    },
    customRowMethods(record, index) {
      return {
        on: {
          mouseenter: (event) => {
            this.$emit("onSelectedRowChange", record);
          },
          mouseleave: (event) => {
            const removeRecord = { id: null };
            this.$emit("onSelectedRowChange", removeRecord);
          },
          click: (event) => {
            this.$emit("onSelectedRowChange", record);
          },
        },
      };
    },
  },
  computed: {
    paginationInfo() {
      return this.pagination
        ? {
            itemRender: this.itemRender,
            current: this.pagination.page,
            pageSize: this.pagination.per_page,
            total: this.pagination.count,
            showTotal: (total, range) =>
              `Hiển thị ${range[0]}-${range[1]} trong ${total}`,
          }
        : {
            itemRender: this.itemRender,
            pageSize: this.pageSizeForClientSide,
            showTotal: (total, range) =>
              `Hiển thị ${range[0]}-${range[1]} trong ${total}`,
          };
    },
  },
  render() {
    const props = {
      columns: this.$props.columns,
      "data-source": this.$props.dataSource,
      pagination: this.paginationInfo,
      // locale: { emptyText: this.emptyText() },
      rowKey: this.$props.rowKey,
      loading: this.$props.loading,
      scroll: {
        x: "1200px",
        // y: this.$props.tableHeight || "65vh",
      },
      customRow: this.customRowMethods,
      components: this.$props.components,
      rowSelection: this.$props.rowSelection,
      size: "small",
      bordered: true,
    };

    const table = (
      <a-table
        {...{ props, scopedSlots: { ...this.$scopedSlots } }}
        onChange={this.handleTableChange}
        onExpand={(expanded, record) => {
          this.$emit("expand", expanded, record);
        }}
      >
        {Object.keys(this.$slots).map((name) => (
          <template slot={name}>{this.$slots[name]}</template>
        ))}
      </a-table>
    );

    return <div id="fc_table">{table}</div>;
  },
};
