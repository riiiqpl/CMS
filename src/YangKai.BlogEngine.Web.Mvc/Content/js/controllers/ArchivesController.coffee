﻿ArchivesController=["$scope","Channel", ($scope,Channel) ->
  $scope.$parent.title='Archives'
  $scope.$parent.showBanner=false

  $scope.load = ->
    Channel.archives (data)->
      obj={}
      for channel in data.value when !channel.IsDeleted
        for group in channel.Groups when !group.IsDeleted
          for post in group.Posts when !post.IsDeleted
            date=moment(post.PubDate).format('YYYY-MM')
            unless obj[date]
              obj[date]=[]
            post.group=group.Name
            post.channel=channel.Name
            obj[date].push(post)

      result=[]
      for key,value of obj
        if obj.hasOwnProperty key
          result.push
            date:key
            posts:value
      $scope.list = result
  $scope.load()
]