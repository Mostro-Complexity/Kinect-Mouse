%% 字段顺序
% handRight.Position.X, handRight.Position.Y,
% handTipRight.Position.X, handTipRight.Position.Y, handTipRight.Position.Z,
% thumbRight.Position.X, thumbRight.Position.Y, thumbRight.Position.Z,
% wristRight.Position.X, wristRight.Position.Y
function tip_filter(path)
%%
src=load(path);
tip=src(:,3:5);
thumb=src(:,6:8);
tip=tip;
thumb=thumb;
%% 计算两个点的距离，单纯A/D
dist=cal_dist(tip,thumb);
figure
subplot(1,2,1);
plot(dist);
hold on
plot(kalman_filter(dist,1e-5,1e-5,dist(1),1));
hold on
plot( a2d(dist,2,0.2,500));
hold off
legend('原数据','卡尔曼','A/D转换');
%% 计算两个点的距离，滤波后A/D
subplot(1,2,2);
plot(dist);
hold on
plot(kalman_filter(dist,1e-5,1e-5,dist(1),1));
hold on
plot( a2d(kalman_filter(dist,1e-5,1e-5,dist(1),1),2,0.2,100));
hold off
legend('原数据','卡尔曼','卡尔曼-A/D');
end