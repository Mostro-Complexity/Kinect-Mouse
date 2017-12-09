figure
star=load('cross.txt');
star=star(:,9:10);
%% 调整Q参数
for i=1:1000
    Q=1e-7*i
    result = [kalman_filter(star(:,1),Q,4e-4,star(1,1),1),...
        kalman_filter(star(:,2),Q,4e-4,star(2,2),1)];
    title('cross');
    plot(star(:,1),star(:,2));
    hold on
    plot(result(:,1),result(:,2));
    hold off
    legend('源数据','卡尔曼滤波');
    pause
end