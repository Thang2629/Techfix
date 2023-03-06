import React, { useState, useEffect } from "react";
import { Table } from "antd";
import * as api from "config/axios";
import { useSelector, useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import _ from "lodash";
import pickBy from "lodash/pickBy";
import identity from "lodash/identity";
import { STORE_ID_KEY, PRODUCT_ASSOCIATED } from "static/Constants";
import { getProductAssicatedByType } from "services/ProductAssociated";
import "./Grid.less";

const GridCashbook = ({
  urlEndpoint,
  tabActive,
  columns,
  data,
  dataFilter,
  isHidePagination,
  ...rest
}) => {
  const dispatch = useDispatch();
  const searchText = useSelector((state) => state.global.searchText);
  const readGrid = useSelector((state) => state.global.refreshGrid);
  const [loading, setLoading] = useState(false);
  const [rowData, setRowData] = useState([]);
  const [tableParams, setTableParams] = useState({
    pagination: {
      pageNumber: 1,
      pageSize: 10,
    },
    FilterParams: [],
  });
  const [selectedIds, setSelectedIds] = useState([]);
  const [storeId, setStoreId] = useState(null)

  useEffect(() => {
    fetchData(searchText);
  }, [searchText]);

  useEffect(() => {
    if (readGrid) {
      fetchData();
    }
  }, [readGrid]);

  const resetState = () => {
    dispatch(actions.refreshGrid(false));
  };

  const fetchData = () => {
    setLoading(true);

    const params = {
      PageSize: tableParams.pagination.pageSize,
      PageNumber: tableParams.pagination.pageNumber,
      FilterParams: tableParams.FilterParams,
    };
    api
      .sendPost(urlEndpoint, params)
      .then((results) => {
        if (results) {
          setRowData(results?.Data); // todo: add params
          if (readGrid) {
            resetState();
          }
        } else {
          setRowData([]);
          setTableParams({
            ...tableParams,
            pagination: {
              ...tableParams.pagination,
              total: 11,
              // 200 is mock data, you should read it from server
              // total: data.totalCount,
            },
            searchText: searchText,
          });
        }
      })
      .catch((err) => {
        console.log("err ne >>> ", err);
      })
      .finally(() => {
        setLoading(false);
      });
  };

  useEffect(() => {
    const filterParams = [];
    const formatFilter = pickBy(dataFilter, identity);
    const fetchStore = async () => {
      const response = await getProductAssicatedByType(
        PRODUCT_ASSOCIATED.STORE
      );
      const storeId = JSON.parse(localStorage.getItem(STORE_ID_KEY))? JSON.parse(localStorage.getItem(STORE_ID_KEY)): response[0].Id;
      setStoreId(storeId)
    }
    fetchStore()
    if (formatFilter.hasOwnProperty("dateFilter") && !_.isEmpty(dataFilter?.dateFilter[0])) {
      filterParams.push({
        PropertyName: "PaymentDate",
        Comparison: ">=",
        Value: dataFilter.dateFilter[0],
      });
      filterParams.push({
        PropertyName: "PaymentDate",
        Comparison: "<=",
        Value: dataFilter.dateFilter[1],
      });
      filterParams.push({
        PropertyName: "IsAdd",
        Comparison: "==",
        Value: tabActive === 1,
      });
      filterParams.push({
        PropertyName: "StoreId",
        Comparison: "==",
        Value: storeId,
      });
    } else {
      filterParams.push({
        PropertyName: "IsAdd",
        Comparison: "==",
        Value: tabActive === 1,
      });
      filterParams.push({
        PropertyName: "StoreId",
        Comparison: "==",
        Value: storeId,
      });
    }
    setTableParams({
      pagination: {
        pageNumber: 1,
        pageSize: 10,
      },
      FilterParams: filterParams,
    });
  }, [tabActive, dataFilter]);

  useEffect(() => {
    urlEndpoint && fetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [JSON.stringify(tableParams)]);

  useEffect(() => {
    data && setRowData(data);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [JSON.stringify(tableParams)]);

  useEffect(() => {
    data && setRowData(data);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [data]);

  const onChange = (pagination, filters, sorter, extra) => {
    setTableParams({
      ...tableParams,
      pagination: {
        ...tableParams.pagination,
        pageIndex: pagination.current,
      },
      sorter: { ...sorter },
      searchText: searchText,
    });
  };

  const rowSelection = {
    onChange: (selectedRowKeys, selectedRows) => {
      console.log(
        `selectedRowKeys: ${selectedRowKeys}`,
        "selectedRows: ",
        selectedRows
      );
      setSelectedIds(selectedRowKeys);
    },
    getCheckboxProps: (record) => ({
      disabled: record.name === "Disabled User", // Column configuration not to be checked
      name: record.name,
    }),
  };

  return (
    <div className="grid">
      <Table
        columns={columns}
        dataSource={rowData}
        onChange={onChange}
        bordered
        pagination={isHidePagination ? false : tableParams.pagination}
        loading={loading}
        rowKey="id"
        rowSelection={{
          ...rowSelection,
        }}
        {...rest}
      />
    </div>
  );
};

GridCashbook.propTypes = {};

export default GridCashbook;
