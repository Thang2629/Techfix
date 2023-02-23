import React, { useState, useEffect } from "react";
import { Table } from "antd";
import * as api from "config/axios";
import { useSelector, useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import "./Grid.less";

const Grid = ({ urlEndpoint, columns, data, isHidePagination, ...rest }) => {
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

  useEffect(() => {
    fetchData(searchText);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [searchText]);

  useEffect(() => {
    if (readGrid) {
      fetchData();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [readGrid]);

  const resetState = () => {
    dispatch(actions.refreshGrid(false));
  };

  const fetchData = (textSearch = "") => {
    setLoading(true);
    const params = {
      FilterParams: tableParams.FilterParams,
      PageSize: tableParams.pagination.pageSize,
      PageNumber: tableParams.pagination.pageNumber,
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

Grid.propTypes = {};

export default Grid;
