import React, { useState, useEffect } from "react";
import { Table } from "antd";
import * as api from "config/axios";
import { useSelector } from "react-redux";
import _ from "lodash";
import pickBy from "lodash/pickBy";
import identity from "lodash/identity";
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
  const [loading, setLoading] = useState(false);
  const [rowData, setRowData] = useState([]);
  const [tableParams, setTableParams] = useState({
    pagination: {
      pageNumber: 1,
      pageSize: 10,
    },
    FilterParams: [],
  });
  const [totalPage, setTotalPage] = useState(0)
  const [selectedIds, setSelectedIds] = useState([]);
  const storeId = useSelector((state) => state.global.storeId);

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
          setTotalPage(results?.TotalCount)     
        } else {
          setRowData([]);
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
  }, [tabActive, dataFilter, storeId]);

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
    });
  };

  const handlePageNumClick = (page) => {
    setTableParams({
      ...tableParams,
      pagination: {
        ...tableParams.pagination,
        pageIndex: page,
      },
    });
  };

  const rowSelection = {
    onChange: (selectedRowKeys, selectedRows) => {
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
        pagination={{
          onChange: handlePageNumClick,
          current: tableParams.pagination.current,
          total: totalPage,
          pageSize: tableParams.pagination.pageSize,
          showSizeChanger: false,
        }}
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